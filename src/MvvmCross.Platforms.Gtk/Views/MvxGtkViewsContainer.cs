using System;
using MvvmCross.Exceptions;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using GtkWidget = Gtk.Widget;
using GtkBuilder = Gtk.Builder;
using GtkApp = Gtk.Application;
using MvvmCross.Platforms.Gtk.Presenters.Attributes;
using System.Reflection;
using System.IO;

namespace MvvmCross.Platforms.Gtk.Views
{
    public class MvxGtkViewsContainer : MvxViewsContainer, IMvxGtkViewsContainer
    {
        private readonly GtkApp _application;

        private IMvxViewModelLoader? _viewModelLoader;
        private IMvxViewModelLoader ViewModelLoader => _viewModelLoader ??= Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();

        public MvxGtkViewsContainer(GtkApp application)
        {
            _application = application;
        }

        public GtkWidget CreateGladeView(MvxViewModelRequest request, IMvxGladeProperties gladeProperties)
        {
            var viewType = GetViewType(request.ViewModelType);
            var builder = gladeProperties switch
            {
                { ObjectId: var id } when id is null => throw new MvxException("ObjectId is missing"),
                { ResourceName: var r} when r is not null => new GtkBuilder(viewType.Assembly, FixResourceName(viewType.Assembly, r), null),
                { FileName: var f } when f is not null => new GtkBuilder(File.OpenRead(f)),
                _ => throw new MvxException($"You must fill-in either ResourceName or FileName in {gladeProperties.GetType().Name}")
            };

            var constructorInfo = viewType.GetConstructor(new[] { typeof(IntPtr), typeof(GtkBuilder) });
            if (constructorInfo is null)
            {
                throw new MvxException($"{viewType.Name} must have one public constructor accepting an IntPtr and a Gtk.Builder");
            }

            var gobject = builder.GetRawObject(gladeProperties.ObjectId);

            var view = constructorInfo.Invoke(new object[] { gobject, builder });

            if (view is IMvxGtkView mvxView)
            {
                mvxView.ViewModel = request switch
                {
                    MvxViewModelInstanceRequest instance => instance.ViewModelInstance,
                    _ => ViewModelLoader.LoadViewModel(request, null)
                };
            }

            if (view is MvxGtkApplicationWindow appWindow)
            {
                appWindow.Application = _application;
            }

            if (gladeProperties.AutoConnectSignals)
            {
                builder.Autoconnect(view);
            }

            return (GtkWidget) view;
        }

        private string FixResourceName(Assembly assembly, string resourceName)
        {
            var assemblyName = assembly.GetName().Name;
            var normalizedResourceName = resourceName.Replace('/', '.');
            return $"{assemblyName}.{normalizedResourceName}";
        }

        public GtkWidget CreateView(MvxViewModelRequest request)
        {
            var viewType = GetViewType(request.ViewModelType);
            if (viewType is null)
            {
                throw new MvxException($"View Type not found for {request.ViewModelType}");
            }

            var widget = CreateView(viewType);
            var gtkView = (IMvxGtkView)widget;

            gtkView.ViewModel = request switch
            {
                MvxViewModelInstanceRequest instance => instance.ViewModelInstance,
                _ => ViewModelLoader.LoadViewModel(request, null)
            };

            return widget;
        }

        public GtkWidget CreateView(Type viewType)
        {
            var viewObject = Mvx.IoCProvider.IoCConstruct(viewType);

            return viewObject switch
            {
                null => throw new MvxException($"View not loaded for {viewType}"),
                GtkWidget widget when widget is IMvxGtkView => widget,
                GtkWidget => throw new MvxException($"Loaded View does not have IMvxGtkView interface {viewType}"),
                _ => throw new MvxException($"Loaded view is not a Gtk.Widget {viewType}")
            };
        }
    }
}

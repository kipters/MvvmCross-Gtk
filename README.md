# MvvmCross Gtk

## What is MvvmCross?

Taken from its README:

> MvvmCross is a cross-platform MVVM framework. It enables developers to create apps using the MVVM pattern on Xamarin.iOS, Xamarin.Android, Xamarin.Mac, Xamarin.Forms, Universal Windows Platform (UWP) and Windows Presentation Framework (WPF). This allows for better code sharing by allowing you to share behavior and business logic between platforms.

Basically, it's an awesome cross-platform MVVM framework. You can read more [here](https://github.com/MvvmCross/MvvmCross).

## What is this repo?

MvvmCross apps are generally composed by two parts: a cross-platform, shared part and a platform specific part.
So is the framework itself. This repo contains an implementation of it for Gtk+3 using [GtkSharp](https://github.com/GtkSharp/GtkSharp).

## What is it for?

I'm building a cross-platform app and since it will have a desktop version, I wanted to have a version on Linux too.
At the same time, I didn't want to use Electron nor spend time to learn how to use it properly since I already have
experience with MvvmCross. 

So I decided it was easier to port MvvmCross to Gtk.

## Will this repo exist forever? What are your plans for it?

Hopefully this will not exist forever. The plan is to contribute it to MvvmCross once it's mature enough.

## Why is a WPF project in there?

It's there in case I need to validate some behavior on an already stable platform.
I used WPF because I think it's the easier to approach between all the desktop platforms supported by MvvmCross.

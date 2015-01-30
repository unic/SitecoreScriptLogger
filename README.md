# Sitecore ScriptLogger

This Sitecore module gives you a very easy and clean way to browse all your Sitecore log files. It allows you to browse all your log files on a local environment and also on a remote authoring or delivery environment. The key point of this module is to be simple, therefor all outputs are in `text/plain`. So there is no fancy filtering or highlighting available ;-)

After the installation of the module, you can browe the log files by calling the following url in your web browser (be sure to be logged in as an administrator):

    http://<your domain>/scriptlogger

This shows you the latest log file. You can also browse each available log files. For additional information and commands, please browse the following url:

    http://<your domain>/scriptlogger/help

## Installation
The module can be installed via NuGet:

    > Install-Package Unic.ScriptLogger

Otherwise you could also clone the repository from GitHub and build it manually. After cloning the repository, the following steps are required:

1. Add Sitecore assemblies into `./lib` directory. Check the `./lib/README.md` file which files are needed.
2. If you wish to deploy the module to a local playground for testing, you can use the VS Publish feature. Open the Solution, right-click on the `Unic.ScriptLogger` Project and select "Publish...". You may want to define the webroot of your playground as target location.

**Note:** We use a private NuGet feed to manage the Sitecore dependencies. This should not be an issue for you, though. We disabled automatic package restore for the project and added the `./lib` directory as an alternative source to check for referenced libraries. So you should be able to build and run the module without issues once you've completed the steps above.

## Supported Sitecore Versions

The module was build and tested on Sitcore 7.2, but due to it's simply functionality it should work on other versions as well.

## Limitation
You need to be logged in as an administrator to use this module. Currently there is no login functionality provided by the module. Usually on delivery environments you also don't have the Sitecore shell. If you don't have a possibility to login, then you could copy the Sitecore login page (`/sitecore/admin/Login.aspx`) to your delivery server.

## License
The Sitecore ScriptLogger is licensed under the LGPL. Please see [LICENSE.txt](LICENSE.txt) for more information.

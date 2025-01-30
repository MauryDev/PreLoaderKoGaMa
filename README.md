# PreLoaderKoGaMa

## Description
PreLoaderKoGaMa is a tool to load plugins before the game starts.

## Features
- Load plugins before game start
- Plugins External
- Custom Configuration

## Installation Process
The installation process of PreLoaderKoGaMa involves the following steps:
1. Downloading the PreLoaderKoGaMa archive from the GitHub repository.
2. Loading the downloaded archive.
3. Extracting the contents of the archive to the specified KoGaMa server directories (BR, WWW, Friends) or a custom path.
4. Patching the `LauncherCore.dll` file in the target directory.
5. Creating necessary directories (`Config` and `Plugins`) in the target directory.
6. Running the `PreLoaderKoGaMa.exe` file with the `install` argument to complete the installation.

## Uninstallation Process
The uninstallation process of PreLoaderKoGaMa involves the following steps:
1. Ensure the PreLoaderKoGaMa archive is available.
2. Run the `PreLoaderKoGaMa.exe` file with the `uninstall` argument in the target directory.
3. Remove the extracted files from the specified KoGaMa server directories (BR, WWW, Friends) or a custom path.
4. Unpatch the `LauncherCore.dll` file in the target directory.
5. Delete the `Config` and `Plugins` directories in the target directory.

## Requirements
- .NET 6
- Mono.Cecil

# Install
https://github.com/MauryDev/PreLoaderKoGaMa/releases/latest

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

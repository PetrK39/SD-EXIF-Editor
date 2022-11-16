# SD EXIF Editor

Tiny tool for viewing and editing image metadata associated with Automatic1111's WebUI StableDiffusion generation parameters

# Getting Started

## Dependencies

- Windows 7+
- .NET 6.0

## Release

Download zip from Release page

## Build

Build using Visual Studio 2022

# Usage

Open the .png file through this application in a convenient way
- Use command line
- Drop the file on `SD EXIF Editor.exe` or on link to it
- (Reccommened) Use FastStone ImageViewer and add `SD EXIF Editor.exe` as External Program. Then use `E` shortcut

If the image contains the 'parameters' tag, its content will be displayed in the text box
It can be modified or deleted/copied using the corresponding hotkeys
The 'used embeddings' part will be ignored because the WebUI cannot handle parsing

# Note

NovelAI metadata not yet supported
Metadata tag name can be changed on build

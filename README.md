# Folder2Iso

A Simple c# utility that allows creating an iso image from a folder. Uses Windows IMAPI API

Usage:

```
Folder2Iso [-u] -i {input folder} -o {output.iso}
Folder2Iso [--udf] --input {input folder} --output {output.iso}
```

`-u` or `--udf` is optional. When specified, the tool will create an UDF file system image.

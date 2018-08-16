# Verify your Event Store database with DBVerifier

The DbVerifier tool lets you verify that your database backups are complete and valid without needing to start a node to do so.

## Download / Install

<!-- TODO: When we have details -->

## Usage

### Verifying

You can verify an entire database by passing the path of a local folder in the `--path` or `-p` parameter:

```powershell
DbVerifier.exe --path C:\es-data
```

Or an individual database chunk:

```powershell
DbVerifier.exe --path C:\es-data\chunk-000005.000000
```

### View chunk headers and footers

You can view the metadata contained in the headers and footers of a database chunk by using the `--headers` or `-h` parameter:

```powershell
DbVerifier.exe --path C:\es-data\chunk-000005.000000 --headers
```

## Further help

For further information you can use the `--help` option.
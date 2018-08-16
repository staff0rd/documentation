# Node administration CLI

This CLI tools is a command line interface for administering Event Store nodes. It allows you to run tasks similar to those available in the web admin interface and SDKs, including administrating users, projections and configuration.

## Installation

- [Download binaries here](#)
- [] Homebrew?
- [] Chocolatey?
- [] Linux packages?

## Usage

```shell
es-cli [options] <command> <sub-command> [args]
```

Where `<command>` is equal to the section, and `<sub-command>` the operation. For example, to shutdown a node:

```shell
es-cli admin shutdown
```

### Connecting and authentication

Using the CLI tool requires specifying an Event Store node by passing the URL with the `--serveurl` option, and your admin credentials with the `--username` and `--password` options:

```bash
es-cli --serverurl="http://localhost:2113" --username=admin --password=changeit
```

## Configuration

You can also create a file with configuration values at the correct file path for your operating system.

### [Windows](#tab/tabid-windows)

```powershell
%AppData%/eventstore.rc
```

### [Linux and macOS](#tab/tabid-linux-macos)

```shell
~/.eventstorerc
```

***

The configuration file may contain any of the following values:

```shell
serverurl="http://127.0.0.1:2113"
username="admin"
password="changeit"
output="json" # Or XML
verbose=true # Or false
```

## Further help

For further information you can use the `--help` option at the `es-cli` top level, the section level, or for individual operations.

The full list of commands and operations available are:

-   Users: `user`
    -   Add: `add`
    -   Delete: `delete`
    -   Update: `update`
    -   List: `list`
    -   Enable and disable: `enable` and `disable`
    -   Change and reset password: `change_password` and `reset_password`
-   Projections: `projections`
    -   Delete: `delete`
    -   Enable and disable: `enable` and `disable`
    -   List: `list`
    -   New: `new`
    -   Result: `result`
    -   State: `state`
    -   Status: `status`
    -   Restore checkpoint: `restore_checkpoint`
    -   Check if stalled: `has_stalled`
-   Subscriptions: `competing`
    -   Create: `create`
    -   Delete: `delete`
    -   Update: `update`
    -   List: `list`
-   Generate configuration: `config_generator`
    - Create: `create_config`
-   Admin: `admin`
    -   Scavenge: `scavenge`
    -   Shutdown: `shutdown`
    -   Calculate stream size: `calculate_stream_size`
    -   Backup and restore: `backup` and `restore`
    -   Backup and restore to S3: `s3_backup` and `s3_restore`
    -   Backup and restore to Azure: `azure_backup` and `azure_restore`
    -   Clear scavenge streams: `clear_scavenge_streams`
    -   Delete streams: `delete_streams`
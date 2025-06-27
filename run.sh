#!/usr/bin/env bash
set -e

dotnet restore
dotnet build --no-restore
dotnet run --no-build

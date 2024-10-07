#!/bin/bash

if [ $# -lt 1 ]; then
  echo "Usage: $0 <filename>.java [args...]"
  exit 1
fi

filename=$(basename -- "$1")
classname="${filename%.*}"

javac "$1"
if [ $? -ne 0 ]; then
  echo "Compilation failed"
  exit 1
fi

shift

java "$classname" "$@"

#!/bin/bash

CWD=$(dirname "$0")
cd "${CWD}" || exit

make_mod_release \
    -e '*/config.xml' '*.user' '*.orig' '*.mdb' '*.pdb' '*.tmp' \
    -i 'saves'

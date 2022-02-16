#!/bin/bash

cd $(dirname "$0")

make_mod_release \
-e '*/config.xml' '*.user' '*.orig' '*.mdb' '*.pdb' '*.tmp' \
-i 'saves'


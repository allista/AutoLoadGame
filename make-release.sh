#!/bin/bash

cd $(dirname "$0")

../../PyKSPutils/make_mod_release \
-e '*/config.xml' '*.user' '*.orig' '*.mdb' '*.tmp' \
-i 'saves'


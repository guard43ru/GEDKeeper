#!/bin/sh

DIR="$( cd "$( dirname "$0" )" && pwd )"

rm -rf ~/rpmbuild/
mkdir -p ~/rpmbuild/BUILD
mkdir -p ~/rpmbuild/RPMS
mkdir -p ~/rpmbuild/SOURCES
mkdir -p ~/rpmbuild/SPECS
mkdir -p ~/rpmbuild/SRPMS

ls -la ../plugins/runtime/
find ../plugins/runtime/ -type d -not -name 'linux-x64' -exec rm -rf {} \;
ls -la ../plugins/runtime/

tar -zcf ~/rpmbuild/SOURCES/gedkeeper.tar.gz -T "$DIR/rpm/gk_files.txt"
cp "$DIR/rpm/gedkeeper.spec" ~/rpmbuild/SPECS/gedkeeper.spec
cd ~/rpmbuild/SPECS/

# build from binary
rpmbuild -bb gedkeeper.spec

cd "$DIR"

ls -la ~/rpmbuild/RPMS/

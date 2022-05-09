#!/bin/zsh

if [ -z "$1" ]
 then
   echo "Version missing"
  exit 1 
fi

if ! dotnet test
then
  echo "Test not passed"
  exit 2
fi

sed -i "" "s$<FileVersion>.*</FileVersion>$<FileVersion>$1</FileVersion>$" ApiKeyFilter/ApiKeyFilter.csproj
sed -i "" "s$<AssemblyVersion>.*</AssemblyVersion>$<AssemblyVersion>$1</AssemblyVersion>$" ApiKeyFilter/ApiKeyFilter.csproj
sed -i "" "s$<PackageVersion>.*</PackageVersion>$<PackageVersion>$1</PackageVersion>$" ApiKeyFilter/ApiKeyFilter.csproj

CURRENT=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')


if ! dotnet build -c release; then
  echo "Build failed"
  exit 102
fi

if [ $CURRENT != 'master' ]; then
  echo "Your branch has to be 'master' for GITy things but is '$CURRENT'"
  exit 1
fi

git add .
git commit -m "Updated version to $1"
git tag -a V$1 -m "Version $1"

git push origin master
git push --tags
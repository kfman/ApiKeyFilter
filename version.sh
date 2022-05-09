#!/bin/zsh

if [ -z "$1" ]; then
  echo "Version missing"
  exit 1
fi

if ! dotnet test; then
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

if [ "$CURRENT" != 'master' ]; then
  if [ "$2" != 'pack' ]; then
    echo "Your branch has to be 'master' for GITy things but is '$CURRENT'"
    exit 1
  fi

  git add .
  git commit -m "Preparing release"
  git push

  if ! git checkout master; then
    echo Git Error - CHECKOUT MASTER
    exit 200
  fi

  if ! git pull; then
    echo Git Error - PULL
    exit 200
  fi

  git merge $CURRENT --no-ff
fi

git add .
git commit -m "Updated version to $1"
git tag -a V$1 -m "Version $1"

git push origin master
git push --tags

if [ "$2" = 'pack' ]; then
  dotnet pack ApiKeyFilter -c release
  dotnet nuget push -s http://172.16.20.73:5110/v3/index.json "./ApiKeyFilter/bin/Release/ApiKeyFilter.$1.nupkg" --api-key ElotecNuGet
fi

if [ "$CURRENT" != 'master' ]; then
  git checkout $CURRENT
  git merge master
fi

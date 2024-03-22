FROM ghcr.io/railwayapp/nixpacks:ubuntu-1707782610

ENTRYPOINT ["/bin/bash", "-l", "-c"]
WORKDIR /app/


COPY .nixpacks/nixpkgs-5148520bfab61f99fd25fb9ff7bfbb50dad3c9db.nix .nixpacks/nixpkgs-5148520bfab61f99fd25fb9ff7bfbb50dad3c9db.nix
RUN nix-env -if .nixpacks/nixpkgs-5148520bfab61f99fd25fb9ff7bfbb50dad3c9db.nix && nix-collect-garbage -d


ARG ASPNETCORE_ENVIRONMENT ASPNETCORE_URLS DOTNET_ROOT NIXPACKS_CSHARP_SDK_VERSION NIXPACKS_METADATA
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT ASPNETCORE_URLS=$ASPNETCORE_URLS DOTNET_ROOT=$DOTNET_ROOT NIXPACKS_CSHARP_SDK_VERSION=$NIXPACKS_CSHARP_SDK_VERSION NIXPACKS_METADATA=$NIXPACKS_METADATA

# setup phase
# noop

# install phase
COPY . /app/.
RUN  dotnet restore

# build phase
COPY . /app/.
RUN  dotnet publish --no-restore -c Release -o out





# start
COPY . /app
CMD ["./out/API_Financeiro_Next"]


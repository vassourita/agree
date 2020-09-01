<p align="center">
   <img src="./.github/assets/agree.svg" alt="Agree Logo" width="300"/>
</p>

<h1 align="center">
  <img alt="License" src="https://img.shields.io/badge/license-MIT-191929?style=flat-square">
  <img alt="Stars" src="https://img.shields.io/github/stars/vassourita/agree?style=flat-square">
  <img alt="Last Commit" src="https://img.shields.io/github/last-commit/vassourita/agree?style=flat-square" />
  <a href="http://standardjs.com">
    <img alt="Code Style" src="https://img.shields.io/badge/code%20style-standard-brightgreen.svg?style=flat-square" />
  </a>
  <br/>
  <img alt="functions coverage" src="./.github/assets/badge-functions.svg">
  <img alt="branches coverage" src="./.github/assets/badge-branches.svg">
  <img alt="lines coverage" src="./.github/assets/badge-lines.svg">
  <img alt="statements coverage" src="./.github/assets/badge-statements.svg">
</h1>

> #### Agree is a voice, video and text communication service based on Discord, currently under development

## :pushpin: Table of Contents

- [Running the project](#computer-running-the-project)
- [Running tests](#pencil-running-tests)
- [License](#pagefacingup-license)

## :computer: Running the project

This section approaches on how to run the project on your own machine

#### Make sure you have installed in your computer:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [NodeJS](https://nodejs.dev/) ^12.13
- [Yarn](https://classic.yarnpkg.com/en/docs/install#debian-stable) (optional)

#### On a terminal open in the project root:

```sh
# clone the repository
$ git clone https://github.com/vassourita/agree.git
# cd into the project folder
$ cd ./agree
# install dependencies
$ yarn
# run docker database containers
$ yarn docker-up
# run accord migrations and server on dev mode
$ yarn accord:dev
# in case you want to finish the docker-compose process
$ yarn docker-down
```

## :pencil: Running tests

```sh
# to run tests
$ yarn test
# to run tests with coverage reports
$ yarn test:cov
# to run tests with coverage reports and update coverage badges
$ yarn test:badges
```

## :page_facing_up: License

This project is licensed under the terms of the [MIT license](/LICENSE).

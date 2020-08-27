/* eslint-disable import-helpers/order-imports */
const { name } = require('./package.json')
const tsconfig = require('./tsconfig.json')

const moduleNameMapper = require('tsconfig-paths-jest')(tsconfig)

module.exports = {
  displayName: name,
  name,
  preset: 'ts-jest',
  moduleFileExtensions: ['js', 'json', 'ts'],
  testRegex: '.spec.ts$',
  transform: {
    '^.+\\.(t|j)s$': 'ts-jest'
  },
  coverageDirectory: '../coverage',
  testEnvironment: 'node',
  moduleNameMapper
}

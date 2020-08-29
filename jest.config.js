module.exports = {
  clearMocks: true,
  preset: 'ts-jest',
  projects: ['<rootDir>/apps/**/jest.config.js'],
  testEnvironment: 'node',
  testMatch: ['*.spec.ts', '*.spec.tsx'],
  coverageReporters: ['json-summary', 'text', 'lcov']
}

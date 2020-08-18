module.exports = {
  clearMocks: true,
  preset: 'ts-jest',
  projects: [
    '<rootDir>/libs/**/jest.config.js',
    '<rootDir>/apps/**/jest.config.js'
  ],
  testEnvironment: 'node',
  testMatch: ['*.spec.ts', '*.spec.tsx']
}

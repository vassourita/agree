module.exports = {
  purge: {
    enabled: true,
    content: [ 
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ]
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
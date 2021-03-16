module.exports = {
  purge: ['./src/pages/**/*.{js,ts,jsx,tsx}', './src/components/**/*.{js,ts,jsx,tsx}'],
  darkMode: false, // or 'media' or 'class'
  theme: {
    fontFamily: {
      sans: ['Sarabun', 'sans-serif']
    },
    colors: {
      primary: '#5A189A',
      background: '#F8F8FB',
      text: '#333333',
      'text-dark': '#F2F2F2',
      button: '#FFFFFF',
      border: '#BDBDBD'
    },
    extend: {
      fontSize: {
        '4.5xl': '2.8rem'
      }
    }
  },
  variants: {
    extend: {}
  },
  plugins: []
}

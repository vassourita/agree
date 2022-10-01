import { extendTheme } from '@chakra-ui/react'

export const theme = extendTheme({
  colors: {
    brand: {
      900: '#201F29',
      800: '#20152F',
      700: '#3C096C',
      600: '#5A189A',
      500: '#7B2CBF',
      400: '#9D4EDD'
    }
  },
  fonts: {
    heading: 'Sarabun',
    body: 'Sarabun'
  },
  config: {
    initialColorMode: 'dark'
  }
})

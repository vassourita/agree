import { extendTheme } from "@chakra-ui/react";

export const chakraTheme = extendTheme({
  colors: {
    primary: "#5A189A",
    secondary: "#9D4EDD",
    tertiary: "#FF6D00",
    background: "#242430",
    background2: "#1A1A24",
    text: "#f0f0f0",
    text2: "#E0E0E0",
    link: "#4D97DB"
  },
  fonts: {
    body: "Sarabun, sans-serif",
    heading: "Sarabun, sans-serif"
  },
  config: {
    initialColorMode: 'dark',
    cssVarPrefix: "comply",
  }
});
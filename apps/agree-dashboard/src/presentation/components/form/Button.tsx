import { Button as ChakraButton } from '@chakra-ui/button'
import { ReactElement, ReactNode } from 'react'

type ButtonProps = {
  rightIcon?: ReactElement
  leftIcon?: ReactElement
  children: ReactNode
  w?: string | string[] | { base?: string, sm?: string, md?: string, lg?: string, xl?: string }
  h?: string | string[] | { base?: string, sm?: string, md?: string, lg?: string, xl?: string }
}

export function Button ({ rightIcon, leftIcon, children, w, h }: ButtonProps): JSX.Element {
  return (
    <ChakraButton
      w={w} h={h}
      bg="brand.600"
      color="gray.100"
      _hover={{ filter: 'brightness(0.90)' }}
      rightIcon={rightIcon}
      leftIcon={leftIcon}
    >
      {children}
    </ChakraButton>
  )
}

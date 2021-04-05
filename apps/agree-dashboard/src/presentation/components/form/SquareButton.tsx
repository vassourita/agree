import { Button, ButtonProps as ChakraButtonProps } from '@chakra-ui/button'
import { ReactNode } from 'react'

type SquareButtonProps = {
  icon: ReactNode
  w: string
} & ChakraButtonProps

export function SquareButton ({ icon, w }: SquareButtonProps): JSX.Element {
  return (
    <Button
    h={w} w={w}
      border="1px" borderColor="gray.200"
      bg="white" color="gray.600"
      _active={{ filter: 'brightness(1.1)', borderColor: 'gray.300' }}
      _focus={{ borderColor: 'gray.300' }}
      _hover={{ borderColor: 'gray.300' }}
    >
      {icon}
    </Button>
  )
}

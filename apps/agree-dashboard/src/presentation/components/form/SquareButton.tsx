import { Button } from '@chakra-ui/button'
import { ReactNode } from 'react'

type SquareButtonProps = {
  icon: ReactNode
  w: string
}

export function SquareButton ({ icon, w }: SquareButtonProps): JSX.Element {
  return (
    <Button
    h={w} w={w}
      border="1px" borderColor="gray.200"
      bg="white" color="gray.600"
      _hover={{ filter: 'brightness(0.96)', borderColor: 'transparent' }}
      _active={{ filter: 'brightness(1.1)', borderColor: 'transparent' }}
    >
      {icon}
    </Button>
  )
}

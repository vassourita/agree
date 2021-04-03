import { Button } from '@chakra-ui/button'
import { ReactNode } from 'react'

type SquareButtonProps = {
  icon: ReactNode
  w: string
}

export function SquareButton ({ icon, w }: SquareButtonProps): JSX.Element {
  return (
    <Button border="1px" borderColor="gray.200" bg="white" h={w} w={w} color="gray.600">
      {icon}
    </Button>
  )
}

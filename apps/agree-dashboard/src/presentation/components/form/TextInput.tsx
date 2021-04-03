import { InputLeftElement, Input, InputGroup } from '@chakra-ui/input'

import { Box } from '@chakra-ui/layout'
import { ReactNode } from 'react'

type TextInputProps = {
  icon: ReactNode
  placeholder: string
  type?: string
}

export function TextInput ({ icon, placeholder, type = 'text' }: TextInputProps): JSX.Element {
  return (
    <InputGroup border="1px" borderColor="gray.200" display="flex" w="300px" h="3.9rem" bg="white" rounded="md" align="center" justifyContent="center">
      <Box w="4px" bg="brand.700" borderLeftRadius="md" />
      <InputLeftElement
        h="full"
        ml="12px"
        color="gray.600"
        fontSize="1.2em"
      >
        {icon}
      </InputLeftElement>
      <Input type={type} fontSize="1.05rem" pl="55px" pb="3px" h="full" placeholder={placeholder} color="gray.600" />
    </InputGroup>
  )
}

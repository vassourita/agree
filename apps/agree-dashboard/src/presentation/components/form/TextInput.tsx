import { InputLeftElement, Input, InputGroup } from '@chakra-ui/input'

import { Box } from '@chakra-ui/layout'
import { ReactNode } from 'react'

type TextInputProps = {
  icon: ReactNode
  placeholder: string
  type?: string
  w?: string | string[] | { base?: string, sm?: string, md?: string, lg?: string, xl?: string }
}

export function TextInput ({ icon, placeholder, type = 'text', w = 'full' }: TextInputProps): JSX.Element {
  return (
    <InputGroup border="1px" borderColor="gray.200" display="flex" w={w} h="3.9rem" bg="white" rounded="md" align="center" justifyContent="center">
      <Box w="4px" bg="brand.700" borderLeftRadius="md" />
      <InputLeftElement
        h="full"
        ml="12px"
        fontSize="1.2em"
        color="gray.600"
      >
        {icon}
      </InputLeftElement>
      <Input
        h="full"
        pl="55px" pb="3px"
        type={type}
        fontSize="1.05rem"
        color="gray.600"
        placeholder={placeholder}
        _placeholder={{ color: 'gray.600' }}
        _focus={{ borderColor: 'transparent' }}
        _active={{ borderColor: 'transparent' }}
      />
    </InputGroup>
  )
}

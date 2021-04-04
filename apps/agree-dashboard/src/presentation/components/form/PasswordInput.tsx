import { useDisclosure } from '@chakra-ui/hooks'
import { InputLeftElement, Input, InputGroup, InputRightElement } from '@chakra-ui/input'

import { Box } from '@chakra-ui/layout'
import { ReactNode } from 'react'
import { FiEye, FiEyeOff } from 'react-icons/fi'

type PasswordInputProps = {
  icon: ReactNode
  placeholder: string
  w?: string | string[] | { base?: string, sm?: string, md?: string, lg?: string, xl?: string }
}

export function PasswordInput ({ icon, placeholder, w = 'full' }: PasswordInputProps): JSX.Element {
  const { isOpen, onToggle } = useDisclosure()

  return (
    <InputGroup border="1px" borderColor="gray.200" display="flex" w={w} h="3.9rem" bg="white" rounded="md" align="center" justifyContent="center">
      <Box w="4px" bg="brand.700" borderLeftRadius="md" />
      <InputLeftElement
        h="full"
        ml="12px"
        color="gray.600"
        fontSize="1.2em"
      >
        {icon}
      </InputLeftElement>
      <Input
        h="full"
        pl="55px" pb="3px"
        type={isOpen ? 'text' : 'password'}
        fontSize="1.05rem"
        color="gray.600"
        rounded="md"
        placeholder={placeholder}
        _placeholder={{ color: 'gray.600' }}
        _focus={{ borderColor: 'transparent' }}
        _active={{ borderColor: 'transparent' }}
      />
      <InputRightElement
        h="full"
        mr="8px"
        color="gray.600"
        fontSize="1.1em"
        onClick={onToggle}
      >
        {isOpen ? <FiEyeOff /> : <FiEye />}
      </InputRightElement>
    </InputGroup>
  )
}

import { useDisclosure } from '@chakra-ui/hooks'
import { InputLeftElement, Input, InputGroup, InputRightElement } from '@chakra-ui/input'

import { Box } from '@chakra-ui/layout'
import { ReactNode } from 'react'
import { FiEye, FiEyeOff } from 'react-icons/fi'

type PasswordInputProps = {
  icon: ReactNode
  placeholder: string
}

export function PasswordInput ({ icon, placeholder }: PasswordInputProps): JSX.Element {
  const { isOpen, onToggle } = useDisclosure()

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
      <Input type={isOpen ? 'text' : 'password'} fontSize="1.05rem" pl="55px" pb="3px" h="full" _placeholder={{ color: 'brand.700' }} placeholder={placeholder} color="gray.600" />
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

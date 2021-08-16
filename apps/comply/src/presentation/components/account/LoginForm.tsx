import Link from "next/link";
import Router from "next/router";
import { Alert, AlertDescription, AlertIcon, AlertTitle, Box, CloseButton, Flex, FormControl, FormHelperText, FormLabel, Heading, Icon, Input, InputGroup, InputRightElement, Text, useDisclosure } from "@chakra-ui/react";
import { ChangeEvent, Dispatch, FormEvent, HTMLProps, SetStateAction, useContext, useState } from "react";
import { AuthContext } from "@logic/contexts/AuthContext";
import { ErrorList } from "@logic/models/ErrorList";
import { Button } from "../form/Button";
import {FiEye, FiEyeOff} from 'react-icons/fi'
import { PasswordInput } from "../form/PasswordInput";

export function LoginForm(props: HTMLProps<HTMLFormElement>) {
  const auth = useContext(AuthContext)

  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [errors, setErrors] = useState<ErrorList>()

  async function handleSubmit() {
    var errors = await auth.login(email, password)
    if (errors) {
      setErrors(errors)
    } else {
      Router.push('/')
    }
  }

  function clearErrors() {
    setErrors(undefined)
  }

  function handleInputChange(event: ChangeEvent<HTMLInputElement>, changer: Dispatch<SetStateAction<string>>) {
    clearErrors()
    changer(event.target.value)
  }

  return (
    <Flex align="center" justify="center" rounded="md" maxW="500px" w="full" px="3rem" py="2.5rem" mt={{ base: '0', md: '3rem' }} bgColor="background">
      <form {...props} style={{width:'100%'}} onSubmit={e => e.preventDefault()}>
        <Box as="header" textColor="text">
          <Heading fontSize="3xl" fontWeight="bold" as="h2">Login</Heading>
          <Text fontSize="lg">Welcome back!</Text>
        </Box>

        {errors && (
          <Alert status="error" mt="2.1rem">
            <AlertIcon />
            {Object.values(errors).flat().map((error, i) => (
              <AlertDescription key={i}>{error}</AlertDescription>
            ))}
          </Alert>
        )}

        <FormControl id="email" w="full" mt="2.1rem">
          <FormLabel fontSize="sm" textTransform="uppercase" fontWeight="medium" textColor="text2">Email</FormLabel>
          <Input color="gray.300" value={email} onChange={v => handleInputChange(v, setEmail)} h="3.5rem" borderColor="background2" bgColor="background2" type="email" w="full" />
        </FormControl>

        <FormControl id="password" w="full" mt="1.3rem">
          <FormLabel fontSize="sm" textTransform="uppercase" fontWeight="medium" textColor="text2">Password</FormLabel>
          <PasswordInput value={password} onChange={e => handleInputChange(e, setPassword)} />
          <FormHelperText textColor="link">
            <Link href="/account/forgot">
              Forgot your password?
            </Link>
          </FormHelperText>
        </FormControl>

        <Button onClick={handleSubmit}>
          Login
        </Button>

        <Flex textColor="text2" w="full" align="center" justify="center" mt="2rem" textAlign="center">
          <Text fontSize="sm">
            Not registered yet?
            <br />
            <Text fontSize="sm" textColor="link">
              <Link href="/account/register">
                Create an account now.
              </Link>
            </Text>
          </Text>
        </Flex>
      </form>
    </Flex>
  )
}
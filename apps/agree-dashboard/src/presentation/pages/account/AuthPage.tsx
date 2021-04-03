import { Box, Container, Flex, Grid, Heading, List, ListItem, Text } from '@chakra-ui/layout'
import { useHistory, useLocation } from 'react-router'
import { Link } from 'react-router-dom'
import { FiArrowLeftCircle, FiChevronRight, FiLock, FiMail, FiUser } from 'react-icons/fi'
import { TextInput } from '../../components/form/TextInput'
import { PasswordInput } from '../../components/form/PasswordInput'
import { SquareButton } from '../../components/form/SquareButton'
import { Slide } from '@chakra-ui/transition'
import { Button } from '../../components/form/Button'
import { useBreakpoints } from '../../hooks/useBreakpoints'

export function AuthPage (): JSX.Element {
  const location = useLocation()
  const history = useHistory()
  const [isBase, isSm, isMd, isLg, isXl] = useBreakpoints()

  const isRegisterPage = location.pathname === '/register'

  return (
    <>
      <Grid
        filter={isRegisterPage ? 'brightness(0.5)' : ''}
        templateAreas={[`
          '. nav nav .'
          'text text text text'
          'form form form form'
        `]}
        templateRows={{ sm: '5rem 1fr auto', lg: '5rem 1fr 13rem' }}
        templateColumns="repeat(1fr, 4)"
        minH="100vh" w="full"
        bg="brand.700"
      >
        <Flex gridArea="nav" as="nav">
          <List fontSize="24" d="flex" hidden mt="3rem" alignItems="end" justifyContent="center" w="full" textAlign="center" gridGap="4rem">
            <ListItem><Link to="/">Página inicial</Link></ListItem>
            <ListItem fontWeight={!isRegisterPage ? 'bold' : 'normal'}><Link to="/login">Login</Link></ListItem>
            <ListItem fontWeight={isRegisterPage ? 'bold' : 'normal'}><Link to="/register">Criar uma conta</Link></ListItem>
          </List>
        </Flex>

        <Flex gridArea="text">
          <Container maxW="80rem">
            <Text fontSize="1.1rem" color="gray.100" lineHeight="1.4">
            </Text>
          </Container>
        </Flex>

        <Flex gridArea="form" as="form" bg="gray.100" align="center" justify="center" py="0">
          <Container maxW="80rem">
            <Flex direction={{ base: 'column-reverse', lg: 'row' }} justify="space-between" align="center">
              <Flex align="center" justify="center" mt={{ base: '2rem', lg: '0' }}>
                <Text fontSize="1.1rem" color="gray.700" lineHeight="1.4">
                  Ainda não tem uma conta?<br />
                  <Link style={{ textDecoration: 'underline' }} to="/register">Clique aqui</Link> para criar uma
                </Text>
              </Flex>
              <Flex direction={{ base: 'column', lg: 'row' }} gridGap={{ base: '1rem', lg: '50px' }}>
                <TextInput w={{ base: 'auto', lg: '300px' }} icon={<FiMail />} placeholder="EMAIL" />
                <PasswordInput w={{ base: 'auto', lg: '300px' }} icon={<FiLock />} placeholder="SENHA" />
                {isMd
                  ? (
                      <Button h="3.9rem" rightIcon={<FiChevronRight />}>LOGIN</Button>
                    )
                  : (
                      <SquareButton w="3.9rem" icon={<FiChevronRight size={20} />} />
                    )}
              </Flex>
            </Flex>
          </Container>
        </Flex>
      </Grid>

      <Slide style={{ zIndex: 10, right: 0, width: 'full', maxWidth: '500px' }} direction="right" in={isRegisterPage}>
        <Box shadow="md" maxW="500px" w="full" h="100vh" p="3rem" bg="gray.100" color="gray.800">
          <Flex w="auto" cursor="pointer" fontWeight="semibold" align="center" gridGap="1rem" onClick={() => history.push('/login')}>
            <FiArrowLeftCircle size={24} />
            VOLTAR
          </Flex>

          <Flex flexDir="column" marginTop="2.5rem">
            <Heading as="h2">Criar conta</Heading>
            <Text marginTop="0.5rem">
              Basta preencher esses dados para criar uma conta. Você poderá personalizar tudo isso depois.
            </Text>
          </Flex>

          <Flex as="form" flexDir="column" marginTop="2.5rem" w="auto" gridRowGap="1.5rem">
            <TextInput icon={<FiUser />} placeholder="NOME DE USUÁRIO" />
            <TextInput icon={<FiMail />} placeholder="EMAIL" />
            <PasswordInput icon={<FiLock />} placeholder="DIGITE SUA SENHA" />
            <PasswordInput icon={<FiLock />} placeholder="CONFIRME SUA SENHA" />
            <Button h="3.9rem" rightIcon={<FiChevronRight />}>LOGIN</Button>
          </Flex>
        </Box>
      </Slide>
    </>
  )
}

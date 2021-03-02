import { motion } from 'framer-motion'
import { FiChevronDown, FiCreditCard, FiDollarSign, FiDownload, FiGithub, FiGlobe, FiHelpCircle, FiLogIn, FiSmartphone } from 'react-icons/fi'
import { AiOutlineQrcode } from 'react-icons/ai'
import { useEffect } from 'react'
import { useMedia } from 'react-use'

export default function Home () {
  const isSmallScreen = useMedia('(max-width: 768px)', false)

  useEffect(() => {
    if (typeof window !== 'undefined') {
      window.scrollTo(0, 0)
    }
  }, [])

  const loadingContainerVariants = {
    hidden: { opacity: 1, scale: 0 },
    visible: {
      opacity: 1,
      scale: 1,
      transition: {
        delayChildren: 0.3,
        staggerChildren: 0.2
      }
    }
  }
  const loadingItemsVariants = {
    hidden: { y: 20, opacity: 0 },
    visible: {
      y: 0,
      opacity: 1
    }
  }

  return (
    <motion.div className="h-screen w-full">
      <motion.header
        initial={{ opacity: 0, x: -200 }}
        animate={{ opacity: 1, x: 0, transition: { ease: 'easeOut' } }}
        className="bg-primary h-full"
      >
        <div className="container h-full m-auto flex flex-col justify-between">

          <nav className="w-full pt-7 md:pt-9 lg:pt-11 flex justify-between items-center">
            <div className="cursor-pointer flex max-w-max ml-6 md:ml-0 transform transition-all hover:scale-110">
              <img src="/agreew.svg" alt="Agree logo" className="w-40 md:w-64 h-auto" />
            </div>

            <motion.div
              initial="hidden"
              animate="visible"
              variants={loadingContainerVariants}
              className="flex text-text-dark gap-4 md:gap-10 mr-6 md:ml-0"
            >
              <motion.a
                variants={loadingItemsVariants}
                href="#about"
              >
                <FiHelpCircle className="cursor-pointer transform transition-all hover:scale-125 h-6 md:h-8 w-auto" />
              </motion.a>
              <motion.a
                variants={loadingItemsVariants}
                href="#dev"
              >
                <FiGithub className="cursor-pointer transform transition-all hover:scale-125 h-6 md:h-8 w-auto" />
              </motion.a>
              <motion.a
                variants={loadingItemsVariants}
                href="#donate"
              >
                <FiDollarSign className="cursor-pointer transform transition-all hover:scale-125 h-6 md:h-8 w-auto" />
              </motion.a>
            </motion.div>
          </nav>

          <motion.div
            initial="hidden"
            animate="visible"
            variants={loadingContainerVariants}
            className="h-full gap-y-1 md:gap-y-0 md:gap-x-5 lg:gap-x-10 md:items-end my-10 md:mt-20 md:py-0 lg:my-10 grid grid-rows-3 md:grid-cols-3 lg:flex"
          >
            <motion.div
              variants={loadingItemsVariants}
              className="flex flex-row md:flex-col items-center justify-center"
            >
              <span className="text-text-dark font-bold mr-4 md:mr-0">Em breve</span>
              <button
                disabled
                className="transform opacity-80 cursor-not-allowed transition-all w-8/12 md:w-full lg:w-64 bg-button flex items-center justify-center px-2 py-3 lg:p-4 lg:p text-base lg:text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                ABRIR NO NAVEGADOR <FiGlobe size="25" className="ml-2 lg:ml-3" />
              </button>
            </motion.div>

            <motion.div
              variants={loadingItemsVariants}
              className="flex flex-row md:flex-col items-center justify-center"
            >
              <span className="text-text-dark font-bold mr-4 md:mr-0">Em breve</span>
              <button
                disabled
                className="transform opacity-80 cursor-not-allowed transition-all w-8/12 md:w-full lg:w-64 bg-button flex items-center justify-center px-2 py-3 lg:p-4 lg:p text-base lg:text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                DOWNLOAD <FiDownload size="24" className="ml-2 lg:ml-3" />
              </button>
            </motion.div>

            <motion.div
              variants={loadingItemsVariants}
              className="flex flex-row md:flex-col items-center justify-center"
            >
              <span className="text-text-dark font-bold mr-4 md:mr-0">Em breve</span>
              <button
                disabled
                className="transform opacity-80 cursor-not-allowed transition-all w-8/12 md:w-full lg:w-64 bg-button flex items-center justify-center px-2 py-3 lg:p-4 lg:p text-base lg:text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                MOBILE <FiSmartphone size="24" className="ml-2 lg:ml-3" />
              </button>
            </motion.div>
          </motion.div>

          <h1 className="text-text-dark font-bold text-4xl md:text-5xl lg:text-7xl pb-5 px-5 md:px-0 md:pb-10 lg:pb-24">
            Te garanto que isso aqui {!isSmallScreen && <br/>}
            definitivamente, {!isSmallScreen && <br/>}
            absolutamente e {!isSmallScreen && <br/>}
            indiscutivelmente {!isSmallScreen && <br/>}
            (não) é uma cópia do Discord.
          </h1>

          <div className="pb-4 md:pb-8 flex items-center justify-center text-text-dark relative">
            <a href="#about" className="cursor-pointer absolute">
              <FiChevronDown size="30" className="hover:scale-125 transform transition-all" />
            </a>
          </div>

        </div>

      </motion.header>

      <main className="bg-background" >
        <div className="container m-auto">
          <section className="pt-32" id="about">
            <h3 className="text-text text-5xl font-bold mb-7">
              Buscando explicações?<br/>
              Sério?
            </h3>
            <p className="text-2xl">
              Quero dizer, você usa outro aplicativo igualzinho todo dia.{!isSmallScreen && <br/>}
              O que quer saber? Se também temos mensagens engraçadas quando alguém entra no servidor?{!isSmallScreen && <br/>}
              Crie uma conta e dexcubra você mesmo.{!isSmallScreen && <br/>}
              Mas tome cuidado com os trolls... Por aqui tá infestado deles...
            </p>
            <div>
              <button className="mt-10 py-4 w-52 bg-button border-border border-solid border text-primary rounded-md flex items-center justify-center font-bold text-lg">
                CRIAR CONTA <FiLogIn className="ml-2" size="24" />
              </button>
            </div>
          </section>

          <section className="pt-32" id="dev">
            <h3 className="text-text text-5xl font-bold mb-7 text-right">
              Como funciona?<br/>
              Finalmente uma pergunta decente
            </h3>
            <p className="text-2xl text-right">
              Uma API em .NET 5 cuida de autenticação e dos servidores, com um banco Postgres por trás.{!isSmallScreen && <br/>}
              Um servidor Elixir cuida das mensagens, vídeos e áudios, usando Redis como banco de baixa latência{!isSmallScreen && <br/>}
              e Postgres para armazenamento geral. Você vê tudo em um PWA feito com React e TypeScript.{!isSmallScreen && <br/>}
              Aonde isso tudo tá rodando? Segredo, mas provavelmente em um contâiner Docker.{!isSmallScreen && <br/>}
              Não entendeu? Dá uma olhada:
            </p>
            <div className="flex justify-end">
              <button className="mt-10 py-4 w-52 bg-button border-border border-solid border text-primary rounded-md flex items-center justify-center font-bold text-lg">
                CÓDIGO FONTE <FiGithub className="ml-2" size="24" />
              </button>
            </div>
          </section>

          <section className="py-32" id="donate">
            <h3 className="text-text text-5xl font-bold mb-7">
              Me dá um reaaaaal?<br/>
              Sério, me ajuda ai :D
            </h3>
            <p className="text-2xl">
            No momento, para manter esse projeto funcionando, tenho um gasto mensal de aproximadamente $00.00 dólares{!isSmallScreen && <br/>}
            por mês. Para um mero estagiário, é f#d@ manter isso. Se você gostou da iniciativa e pode me ajudar,{!isSmallScreen && <br/>}
            por favor, considere uma doação.{!isSmallScreen && <br/>}
            Ah, e você ganha uma recompensa na plataforma por isso.
            </p>
            <div className="flex gap-10">
              <button className="mt-10 py-4 w-52 bg-button border-border border-solid border text-primary rounded-md flex items-center justify-center font-bold text-lg">
                PIX <AiOutlineQrcode className="ml-2" size="24" />
              </button>
              <button className="mt-10 py-4 w-52 bg-button border-border border-solid border text-primary rounded-md flex items-center justify-center font-bold text-lg">
                PICPAY <FiCreditCard className="ml-2" size="24" />
              </button>
            </div>
          </section>
        </div>
      </main>

      <footer className="bg-primary">
        <div className="flex items-center justify-center py-6">
          <p className="text-text-dark text-lg">Agree 2021 - Nenhum direito reservado :(</p>
        </div>
      </footer>
    </motion.div>
  )
}

import { motion, useTransform, useViewportScroll } from 'framer-motion'
import { FiChevronDown, FiDollarSign, FiDownload, FiGithub, FiGlobe, FiHelpCircle, FiSmartphone } from 'react-icons/fi'
import { useEffect } from 'react'
import { useMedia } from 'react-use'

export default function Home () {
  const isSmallScreen = useMedia('(max-width: 768px)', false)
  const { scrollYProgress } = useViewportScroll()
  const opacity = useTransform(scrollYProgress, [0, 1], [0.2, 1])
  console.log(opacity)

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
        initial={{ opacity: 0 }}
        animate={{ opacity: opacity.get() }}
        transition={{
          type: 'just'
        }}
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
                variants={loadingItemsVariants}MotionValue
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
                className="transform opacity-80 cursor-not-allowed transition-all w-8/12 md:w-full lg:w-64 bg-button flex items-center justify-center px-2 py-3 lg:p-4 text-base lg:text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                Abrir no navegador <FiGlobe size="25" className="ml-2 lg:ml-3" />
              </button>
            </motion.div>

            <motion.div
              variants={loadingItemsVariants}
              className="flex flex-row md:flex-col items-center justify-center"
            >
              <span className="text-text-dark font-bold mr-4 md:mr-0">Em breve</span>
              <button
                disabled
                className="transform opacity-80 cursor-not-allowed transition-all w-8/12 md:w-full lg:w-64 bg-button flex items-center justify-center px-2 py-3 lg:p-4 text-base lg:text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                Download <FiDownload size="24" className="ml-2 lg:ml-3" />
              </button>
            </motion.div>

            <motion.div
              variants={loadingItemsVariants}
              className="flex flex-row md:flex-col items-center justify-center"
            >
              <span className="text-text-dark font-bold mr-4 md:mr-0">Em breve</span>
              <button
                disabled
                className="transform opacity-80 cursor-not-allowed transition-all w-8/12 md:w-full lg:w-64 bg-button flex items-center justify-center px-2 py-3 lg:p-4 text-base lg:text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                Mobile <FiSmartphone size="24" className="ml-2 lg:ml-3" />
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
              <FiChevronDown size="24" className="hover:scale-125 transform transition-all" />
            </a>
          </div>

        </div>

      </motion.header>

      <main className="min-h-screen bg-background" >
        <div className="container m-auto">
          <section id="about">
            <h3></h3>
            <p></p>
          </section>

          <section id="dev">
            <h3></h3>
            <p></p>
          </section>

          <section id="donate">
            <h3></h3>
            <p></p>
          </section>
        </div>
      </main>
    </motion.div>
  )
}

import { motion } from 'framer-motion'
import { ReactNode } from 'react'

type HomeContainerProps = {
  children: ReactNode
}

export function HomeContainer ({ children }:HomeContainerProps) {
  return (
    <motion.div className="h-screen w-full">
      {children}
    </motion.div>
  )
}

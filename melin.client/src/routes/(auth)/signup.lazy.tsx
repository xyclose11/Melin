import { createLazyFileRoute } from '@tanstack/react-router'
import SignUpForm from '@/SignUp.tsx'

export const Route = createLazyFileRoute('/(auth)/signup')({
  component: SignUpForm,
})

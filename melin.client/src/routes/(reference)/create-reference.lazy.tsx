import { createLazyFileRoute } from '@tanstack/react-router'
import { CreateReferencePage } from '@/CreateReferencePage.tsx'

export const Route = createLazyFileRoute('/(reference)/create-reference')({
  component: CreateReferencePage,
})

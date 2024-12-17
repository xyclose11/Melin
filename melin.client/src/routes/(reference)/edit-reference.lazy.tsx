import { createLazyFileRoute } from '@tanstack/react-router'
import { EditReferencePage } from '@/Reference/EditReferencePage.tsx'

export const Route = createLazyFileRoute('/(reference)/edit-reference')({
  component: EditReferencePage,
})

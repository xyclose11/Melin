import { createLazyFileRoute } from '@tanstack/react-router'

export const Route = createLazyFileRoute('/(auth)/signup')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/signup"!</div>
}

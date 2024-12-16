import { createLazyFileRoute } from '@tanstack/react-router'

export const Route = createLazyFileRoute('/reset-password')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/reset-password"!</div>
}

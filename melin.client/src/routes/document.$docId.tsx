import { createFileRoute } from "@tanstack/react-router";
import { Button } from "@/components/ui/button.tsx";

export const Route = createFileRoute("/document/$docId")({
    component: DocumentComponent,
});

function DocumentComponent() {
    return (
        <div>
            <Button>Send Message</Button>
        </div>
    );
}

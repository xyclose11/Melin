import { createFileRoute } from "@tanstack/react-router";
import { Button } from "@/components/ui/button.tsx";
import Connector from "@/DocumentEditing/documentActions.ts";
import { useEffect, useState } from "react";
import { fetchDocument } from "@/api/fetchDocument.ts";

export const Route = createFileRoute("/document/$fileName")({
    loader: async ({ params }) => {
        return fetchDocument(params.fileName);
    },
    component: DocumentComponent,
});

function DocumentComponent() {
    const { newMessage, events } = Connector();
    const [message, setMessage] = useState("initial value");

    useEffect(() => {
        const handleMessageReceived = (_: any, message: string) =>
            setMessage(message);

        const handleNewConnectionReceived = () => {};

        events(handleMessageReceived);
    }, []);

    return (
        <div>
            <span>
                message from signalR:{" "}
                <span style={{ color: "green" }}>{message}</span>{" "}
            </span>

            <Button onClick={() => newMessage(new Date().toISOString())}>
                Send Message
            </Button>
        </div>
    );
}

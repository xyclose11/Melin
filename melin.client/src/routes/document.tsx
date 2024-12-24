import { createFileRoute } from "@tanstack/react-router";
import { Button } from "@/components/ui/button.tsx";
import Connector from "../DocumentEditing/documentActions.ts";
import { useEffect, useState } from "react";
export const Route = createFileRoute("/document")({
    component: DocumentComponent,
});

function DocumentComponent() {
    const { newMessage, events } = Connector();
    const [message, setMessage] = useState("initial value");

    useEffect(() => {
        console.log(message);
        events((_, message) => setMessage(message));
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

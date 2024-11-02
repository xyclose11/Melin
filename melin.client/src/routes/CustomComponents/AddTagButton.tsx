import { Plus } from "lucide-react";
import { Button } from "@/components/ui/button.tsx";

export function AddTagButton() {
    return (
        <>
            <div>
                <Button className={"h-6 p-0 w-6"}>
                    <Plus size={16} color="#ffffff" strokeWidth={1} />
                </Button>
            </div>
        </>
    );
}

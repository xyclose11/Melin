import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";
import { ReferenceType } from "@/Import/ImportFile.tsx";
import { Button } from "@/components/ui/button.tsx";
import { SquareXIcon } from "lucide-react";

export function FormattedView({
    data,
    name,
    handleRemoveReference,
}: {
    data: ReferenceType;
    name: string;
    handleRemoveReference: (idx: number) => void;
}) {
    console.log(data);
    return (
        <div>
            <Card>
                <CardHeader>
                    <CardTitle>Formatted View</CardTitle>
                    <CardDescription>
                        Here is the formatted view!
                    </CardDescription>
                </CardHeader>
                <CardContent>{data.title}</CardContent>
                <CardFooter className="justify-end">
                    <Button
                        variant="destructive"
                        className="p-1.5 m-0"
                        onClick={() =>
                            handleRemoveReference(parseInt(name.slice(13)))
                        }
                        type="button"
                    >
                        <SquareXIcon />
                    </Button>
                </CardFooter>
            </Card>
        </div>
    );
}

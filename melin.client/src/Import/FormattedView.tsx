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
    return (
        <div className="mt-2">
            <Card>
                <CardHeader>
                    <CardTitle>{data.title}</CardTitle>
                    <CardDescription>
                        {data.type
                            .slice(0, 1)
                            .toUpperCase()
                            .concat(data.type.slice(1, data.type.length))}
                    </CardDescription>
                </CardHeader>
                <CardContent>
                    <div>
                        <p>
                            <label className="font-bold text-secondary">
                                Edition:{" "}
                            </label>
                            {data.edition}
                        </p>
                        <p>
                            <label className="font-bold text-secondary">
                                Authors:{" "}
                            </label>
                            <ul>
                                {data.author.map((a) => {
                                    return (
                                        <li>
                                            {a.family}, {a.given}
                                        </li>
                                    );
                                })}
                            </ul>
                        </p>
                        <p>
                            <label className="font-bold text-secondary">
                                Publisher:{" "}
                            </label>
                            {data.publisher}
                        </p>
                        <p>
                            <label className="font-bold text-secondary">
                                ISBN:{" "}
                            </label>
                            {data.ISBN}
                        </p>
                    </div>
                </CardContent>
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

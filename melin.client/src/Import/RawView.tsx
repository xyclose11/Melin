﻿import { Card, CardContent, CardFooter } from "@/components/ui/card.tsx";
import {
    FormControl,
    FormField,
    FormItem,
    FormMessage,
} from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import { useFormContext } from "react-hook-form";
import { Button } from "@/components/ui/button.tsx";

export function RawView({
    name,
    handleRemoveReference,
}: {
    name: string;
    handleRemoveReference: (idx: number) => void;
}) {
    const { control, register, getValues } = useFormContext();

    console.log();
    return (
        <div className="mt-2">
            <Card className="w-[100%] pt-5 bg-accent">
                <CardContent>
                    <FormField
                        control={control}
                        name={`${name}.value`}
                        render={() => (
                            <FormItem>
                                <FormControl>
                                    <Textarea
                                        placeholder=""
                                        className="min-h-[250px] resize-none p-2.5 w-full text-sm text-gray-900 rounded-lg border border-gray-300"
                                        defaultValue={""}
                                        {...register(`${name}.value` as const)}
                                    />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                </CardContent>
                <CardFooter>
                    <Button
                        onClick={() =>
                            handleRemoveReference(parseInt(name.slice(13)))
                        }
                    ></Button>
                </CardFooter>
            </Card>
        </div>
    );
}

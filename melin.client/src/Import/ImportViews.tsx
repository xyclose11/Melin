﻿import {
    Tabs,
    TabsList,
    TabsContent,
    TabsTrigger,
} from "@/components/ui/tabs.tsx";
import { FormattedView } from "@/Import/FormattedView.tsx";
import { RawView } from "@/Import/RawView.tsx";
import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { Button } from "@/components/ui/button.tsx";
import { ReferenceType } from "@/Import/ImportFile.tsx";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { DevTool } from "@hookform/devtools";

const rawFormSchema = z.object({
    rawDataArray: z.array(z.string()).optional(),
});
export function ImportViews({
    rawData,
    formattedData,
}: {
    rawData: string[];
    formattedData: ReferenceType[];
}) {
    const methods = useForm<z.infer<typeof rawFormSchema>>({
        resolver: zodResolver(rawFormSchema),
        defaultValues: { rawDataArray: [] },
    });

    const { fields, append } = useFieldArray({
        control: methods.control,
        name: "rawDataArray",
    });
    const onSubmit = (values: any) => {
        console.log(values);
    };

    useEffect(() => {
        console.log(rawData);
        if (rawData.length > 0) {
            rawData.forEach((data) => append(data));
        }
    }, [rawData]);

    return (
        <div>
            <FormProvider {...methods}>
                <form onSubmit={methods.handleSubmit(onSubmit)}>
                    <Card className="size-full">
                        <CardHeader className="">
                            <CardTitle className="p-0 bg-sky-500/100">
                                Parsed Data
                            </CardTitle>
                        </CardHeader>
                        <CardContent className="justify-start p-5">
                            <Tabs defaultValue="formatted" className="p-4">
                                <TabsList>
                                    <TabsTrigger value="formatted">
                                        Formatted
                                    </TabsTrigger>
                                    <TabsTrigger value="raw">Raw</TabsTrigger>
                                </TabsList>
                                <TabsContent value="formatted">
                                    {formattedData.map((f) => (
                                        <FormattedView key={f.id} data={f} />
                                    ))}
                                </TabsContent>
                                <TabsContent value="raw">
                                    {fields.map((item, index) => {
                                        return (
                                            <RawView
                                                key={item.id}
                                                name={`rawDataArray.${index}`}
                                            />
                                        );
                                    })}
                                </TabsContent>
                            </Tabs>
                        </CardContent>
                        <CardFooter className="gap-4 p-4 justify-end">
                            <Button
                                className="p-2"
                                type="reset"
                                variant="destructive"
                            >
                                Cancel
                            </Button>
                            <Button
                                className="p-2"
                                type="submit"
                                variant="secondary"
                            >
                                Import
                            </Button>
                        </CardFooter>
                    </Card>
                </form>
            </FormProvider>
            <DevTool control={methods.control} />
        </div>
    );
}

﻿namespace FSharp.Plotly

[<AutoOpen>]
module ChartExtensions =


    open System
    open System.IO

    open StyleGramar
    open ChartArea

    open GenericChart



    type Chart with   
        
        static member withMarker(marker:Marker) =
            (fun (ch:GenericChart) ->  
                ch 
                |> GenericChart.map (fun gc -> 
                                        gc.set_marker marker
                                        gc)
            )
               
        static member withMarkerStyle(?Size,?Color,?Symbol,?Opacity) = 
            let marker = 
                Marker()                
                |> Helpers.ApplyMarkerStyles(?size=Size,?color=Color,?symbol=Symbol,?opacity=Opacity)
            
            Chart.withMarker(marker)         
            
               
        static member withLine(line:Line) =
            (fun (ch:GenericChart) ->  
                ch 
                |> GenericChart.map (fun gc -> 
                                        gc.set_line line
                                        gc)
            )
               
        static member withLineStyle(?Width,?Color,?Shape,?Dash,?Smoothing,?ColorScale) =
            let line = 
                Line()                
                |> Helpers.ApplyLineStyles(?width=Width,?color=Color,?shape=Shape,?dash=Dash,?smoothing=Smoothing,?colorScale=ColorScale)
            
            Chart.withLine(line)  


        // -------------
        // with Layout
        static member withLayout(layout:Layout) =
            (fun (ch:GenericChart) -> 
                GenericChart.setLayout layout ch)         


        static member withAxis(layout:Layout) =
            (fun (ch:GenericChart) -> 
                GenericChart.setLayout layout ch)   

        // with Layout
        static member withError(layout:Layout) =
            (fun (ch:GenericChart) -> 
                GenericChart.setLayout layout ch)   
            
        static member Show (ch:GenericChart) = 
            let guid = Guid.NewGuid().ToString()
            let html = GenericChart.toHTML ch
            let tempPath = Path.GetTempPath()
            let file = sprintf "%s.html" guid
            let path = Path.Combine(tempPath, file)
            File.WriteAllText(path, html)
            System.Diagnostics.Process.Start(path) |> ignore

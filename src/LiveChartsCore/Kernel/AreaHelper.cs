﻿// The MIT License(MIT)

// Copyright(c) 2021 Alberto Rodriguez Orozco & LiveCharts Contributors

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using LiveChartsCore.Drawing;
using System;

namespace LiveChartsCore.Kernel
{
    public class AreaHelper<TDrawingContext, TGeometryPath, TLineSegment, TMoveTo, TPathContext>
        where TGeometryPath : IPathGeometry<TDrawingContext, TPathContext>, new()
        where TLineSegment : ILinePathSegment<TPathContext>, new()
        where TMoveTo : IMoveToPathCommand<TPathContext>, new()
        where TDrawingContext : DrawingContext
    {
        public IPathGeometry<TDrawingContext, TPathContext> Path { get; set; } = new TGeometryPath();

        public IMoveToPathCommand<TPathContext> StartPoint { get; set; } = new TMoveTo();

        public ILinePathSegment<TPathContext> StartSegment { get; set; } = new TLineSegment();

        public ILinePathSegment<TPathContext> EndSegment { get; set; } = new TLineSegment();

        public bool IsInitialized { get; set; }

        public bool Initialize(
            Action<AreaHelper<TDrawingContext, TGeometryPath, TLineSegment, TMoveTo, TPathContext>, Animation> transitionSetter,
            Animation defaultAnimation)
        {
            if (IsInitialized) return false;

            IsInitialized = true;
            transitionSetter(this, defaultAnimation);

            return true;
        }
    }
}
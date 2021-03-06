﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.


using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;

using Xunit.Abstractions;

namespace SixLabors.ImageSharp.Tests
{

    /// <summary>
    /// Provides <see cref="Image{TPixel}" /> instances for parametric unit tests.
    /// </summary>
    /// <typeparam name="TPixel">The pixel format of the image</typeparam>
    public abstract partial class TestImageProvider<TPixel>
        where TPixel : struct, IPixel<TPixel>
    {
        private class SolidProvider : BlankProvider
        {
            private byte a;

            private byte b;

            private byte g;

            private byte r;

            public SolidProvider(int width, int height, byte r, byte g, byte b, byte a)
                : base(width, height)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            public SolidProvider()
                : base()
            {
                this.r = 0;
                this.g = 0;
                this.b = 0;
                this.a = 0;
            }

            public override string SourceFileOrDescription
                => $"Solid{this.Width}x{this.Height}_({this.r},{this.g},{this.b},{this.a})";

            public override Image<TPixel> GetImage()
            {
                Image<TPixel> image = base.GetImage();
                TPixel color = default(TPixel);
                color.PackFromRgba32(new Rgba32(this.r, this.g, this.b, this.a));

                image.Mutate(x => x.Fill(color));
                return image;
            }

            public override void Serialize(IXunitSerializationInfo info)
            {
                info.AddValue("red", this.r);
                info.AddValue("green", this.g);
                info.AddValue("blue", this.b);
                info.AddValue("alpha", this.a);
                base.Serialize(info);
            }

            public override void Deserialize(IXunitSerializationInfo info)
            {
                this.r = info.GetValue<byte>("red");
                this.g = info.GetValue<byte>("green");
                this.b = info.GetValue<byte>("blue");
                this.a = info.GetValue<byte>("alpha");
                base.Deserialize(info);
            }
        }
    }
}
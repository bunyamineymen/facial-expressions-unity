#nullable enable
using System.Collections.Generic;
using Mochineko.FacialExpressions.Blink;
using UniVRM10;

namespace Mochineko.FacialExpressions.Extensions.VRM
{
    public sealed class VRMEyelidMorpher : IEyelidMorpher
    {
        private readonly Vrm10RuntimeExpression expression;

        private static readonly IReadOnlyDictionary<Eyelid, ExpressionKey> KeyMap
            = new Dictionary<Eyelid, ExpressionKey>
            {
                [Eyelid.Both] = ExpressionKey.Blink,
                [Eyelid.Left] = ExpressionKey.BlinkLeft,
                [Eyelid.Right] = ExpressionKey.BlinkRight,
            };

        public VRMEyelidMorpher(Vrm10RuntimeExpression expression)
        {
            this.expression = expression;
        }

        public void MorphInto(EyelidSample sample)
        {
            if (KeyMap.TryGetValue(sample.eyelid, out var key))
            {
                expression.SetWeight(key, sample.weight);
            }
        }

        public void Reset()
        {
            MorphInto(new EyelidSample(Eyelid.Both, weight: 0f));
            MorphInto(new EyelidSample(Eyelid.Left, weight: 0f));
            MorphInto(new EyelidSample(Eyelid.Right, weight: 0f));
        }
    }
}
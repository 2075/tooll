// Copyright (c) 2016 Framefield. All rights reserved.
// Released under the MIT license. (see LICENSE.txt)

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framefield.Core.Curve
{
    public class CurveState : IOperatorPartState
    {
        public bool Changed { get; set; }
        public SortedList<double, VDefinition> Table { get; set; }

        public Utils.OutsideCurveBehavior PreCurveMapping
        {
            get
            {
                return _preCurveMapping;
            }
            set
            {
                _preCurveMapping = value;
                PreCurveMapper = Utils.CreateOutsideCurveMapper(value);
                Changed = true;
            }
        }
        public Utils.OutsideCurveBehavior PostCurveMapping
        {
            get
            {
                return _postCurveMapping;
            }
            set
            {
                _postCurveMapping = value;
                PostCurveMapper = Utils.CreateOutsideCurveMapper(value);
                Changed = true;
            }
        }

        public IOutsideCurveMapper PreCurveMapper { get; private set; }
        public IOutsideCurveMapper PostCurveMapper { get; private set; }

        public CurveState()
        {
            Table = new SortedList<double, VDefinition>();
            PreCurveMapping = Utils.OutsideCurveBehavior.Constant;
            PostCurveMapping = Utils.OutsideCurveBehavior.Constant;
            Changed = false;
        }

        public IOperatorPartState Clone()
        {
            var clone = new CurveState();
            clone.PreCurveMapping = _preCurveMapping;
            clone.PostCurveMapping = _postCurveMapping;

            foreach (var point in Table)
                clone.Table[point.Key] = point.Value.Clone();

            clone.Changed = true;

            return clone;
        }

        private Utils.OutsideCurveBehavior _preCurveMapping;
        private Utils.OutsideCurveBehavior _postCurveMapping;
    }

}
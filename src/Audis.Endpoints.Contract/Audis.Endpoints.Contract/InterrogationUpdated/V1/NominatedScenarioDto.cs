﻿using System;
using System.Collections.Generic;
using System.Text;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V1
{
    public class NominatedScenarioDto
    {
        public ScenarioIdentifier ScenarioIdentifier { get; set; }
        public int NominatedAtProcessStepId { get; set; }
    }
}

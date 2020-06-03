#include "pch.h"

//#define INIT_CLASS_IID
// This macro definition modifies the behavior of DECLARE_CLASS_IID (funknown.h)
// and produces the actual symbols for all interface identifiers.
// It must be defined before including the interface headers and
// in only one source file!
//------------------------------------------------------------------------
#define INIT_CLASS_IID

#include <pluginterfaces/base/conststringtable.h>
#include <pluginterfaces/base/fplatform.h>
#include <pluginterfaces/base/fstrdefs.h>
#include <pluginterfaces/base/ftypes.h>
#include <pluginterfaces/base/funknown.h>
#include <pluginterfaces/base/futils.h>
#include <pluginterfaces/base/ibstream.h>
#include <pluginterfaces/base/icloneable.h>
#include <pluginterfaces/base/ierrorcontext.h>
#include <pluginterfaces/base/ipersistent.h>
#include <pluginterfaces/base/ipluginbase.h>
#include <pluginterfaces/base/istringresult.h>
#include <pluginterfaces/base/iupdatehandler.h>
#include <pluginterfaces/base/keycodes.h>
#include <pluginterfaces/base/ustring.h>
